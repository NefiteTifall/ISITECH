# VroomVroom

Cette plateforme est un projet simple d'école, vioci l'explication :

Le systeme de vente de vehicules gere des vehicules qui fonctionnent soit de maniere electrique soit avec de l'essence. La gestion de ce mecanisme sera gere par la l'objet `Catalogue`. L'objet `Catalogue` se charge alors de cree de tels objets (vehicules).

Pour chacun des prduits, nous disposons d'une **classe abstraite**, et sous-classe qui decrit la version electrique et la version essence.

Si l'objet `Catalogue` utilise les sous classes pour instancier les produits, on devra apporter des modifications a la classe catalogue lors de l'ajout de chaque nouveau produit.

Le pattern **Abstract Factory** resout cette problematique en introduisant une interface `FabriqueVehicule` qui contient la signature des methodes pour definir chaque produit. Le type de retour de ces methodes est constitué par l'une des classes abstraites de produit. L'objet Catalogue n'a alors pas besoin de connaitre les classes concretes des produits.

Une sous-classe d’implementation de `FabriqueVehicule` est introduite pour chaque famille de produit, à savoir les sous-classes `FabriqueVehiculeEssence` et `FabriqueVehiculeElectrique`. Une telle sous-classe implante les opérations de création du véhicule appropriée pour la famille à laquelle elle est associée.

L’objet prend alors pour paramètre une instance répondant à l’interface, c’est-à-dire soit une instance de `FabriqueVehiculeElectrique` , soit une instance de `FabriqueVehiculeEssence` . Avec une telle instance, le `Catalogue` peut créer et manipuler des véhicules sans devoir connaître les familles de véhicule et les classes concrètes d’instanciation correspondantes.