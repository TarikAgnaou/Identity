base de données
	RoleClaims  => permissions pour les rôles
	Roles		=> définitions des rôles
	UserClaims	=> permissions pour les utilisateurs
	UserLogins	=> logins pour les utilisateurs
	UserRoles	=> association des rôles aux utilisateurs
	Users		=> définitions des utilisateurs
	UserToken	=> stockage des jetons pour les utilisateurs

-------------------------------------------------------------------------------------------

2 services
	UserManager<TUser>
		gestion des utilisateurs :
			CreateAsync()
			UpdateAsync()
		gestion des mots de passe :
			CheckPassword()
			HasPassword()
			AddPassword()
		gestion des token :
			GestSecurityStamp()
			UpdateSecurityStamp()
		gestion multicompte :
			AddLogin()
			RemoveLogin()
			GetLogins()
		gestion des roles :
			IsInRole()
			GetRoles()
			AddToRole()
		gestion des confirmations
			GenerateEmailConfirmationToken()
	SignInManager<TUser>
		connexion :
			SignIn()
			PasswordSignIn() => comme SignIn mais vérifie le mot de passe
			TwoFactorSignIn()
			ExternalLoginSignIn()

-------------------------------------------------------------------------------------------
			
Autorisation simple ou autorisation basée sur :
	- des rôles
	- des revendications
	- des policy
	- des ressources
	- les vues

-------------------------------------------------------------------------------------------
	
Installer le NuGet:
	Microsoft.AspNetCore.Identity.EntityFrameworkCore
	Microsoft.EntityFrameworkCore.Tools
	Microsoft.EntityFrameworkCore.Design
	Microsoft.EntityFrameworkCore.SqlServer

-------------------------------------------------------------------------------------------
	
Ajouter à AppSettings.json & AppSettings.Development.json entre les 2 dernières accolades

,
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LinkedInSecurity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }

-------------------------------------------------------------------------------------------

Dans le fichier cjproj du projet ajouter la ligne de commande CLI pour faire les migrations & update de la base de données
Se mettre dans le dossier du sous-projet pour que la commande soit opérationnel

<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />

-------------------------------------------------------------------------------------------

Créer des classes qui hériteront des classe de base de Identity si on veut des champs supplémentaire
Les classes de base sont les tables de la BD repris plus haut

User => IdentityUser<>

using Microsoft.AspNetCore.Identity;
using System;
namespace LinkendInSecurity.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

-------------------------------------------------------------------------------------------

Créer la DbContext qui heritera de IdentityDbContext<User,Role,Guid>
Ne pas oublié les 2 méthodes 
	- OnConfiguring
	- OnModelCreating
Qui appelleront les méthodes de la classe hérité (base)

-------------------------------------------------------------------------------------------

Dans le controller passer les 2 paramétres suivants :

	- UserManager<User> => permet de gérer les user dans la BD
	- SignInManager<User> => permet de vérifier les user dans la BD

-------------------------------------------------------------------------------------------

Pour ajouter des authorisation lié au claims il faut créer 
	- un Requirement => determine les données à vérifier
	- un Handler => vérifie les données du User avec données détermminées dans Requirement via le startup.cs

-------------------------------------------------------------------------------------------

Décorer les Controller des balises authorization
    [Authorize(Roles = "Admin, Salarié")]
    [Authorize(Policy = "AtLeast21")]

Placer les If dans les layout pour authorisé les vues
	@if (User.Identity.IsAuthenticated)
	if (User.IsInRole("Admin")

