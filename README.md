# SGA - Twix Gaming

## Etape 1

Le joueur est sur une plate-forme avec une épée, il peut se déplacer et attaquer un ennemi. Si l'enemi est touché, il disparait.

### A implémenter

- Le joueur peut se déplacer sur les côtés ou se déplacer sur la plateforme en se téléportant.
- Il peut attaquer avec son arme.
- l'arme entre en contact avec l'ennemi
- quand l'ennemi est touché par l'épée il disparait

### modèles 3d

- une épée (modèle simple)
- un ennemi (sans attaque)



## Etape 2

l'ennemi attaque le joueur en se dirigeant vers lui, s'il le touche il lui fait perdre une vie. si le joueur est touché 3 fois, il perd la partie

### A implémenter

- Le joueur peut lancer une partie en appuyant sur un bouton
- Le joueur a un certain nombre de vies.
- il y a plusieurs ennemis qui s'instancient (à un point fixe) les uns après les autres et qui se dirigent vers le joueur.


- Le joueur peut perdre
- S'il a perdu ses trois vies, il voit apparaître l'écran le fin avec la possibilité de relancer une nouvelle partie.

### Ressources graphiques

- écran de lancement de jeu

- écran de fin de jeu 

  ​

## Etape 3

Un ennemi vaincu rapporte des points. A la fin du jeu, le score du joueur est affiché. 

### A implémenter

- un ennemi est associé à un certain nombre de points.
- Si l'ennemi est vaincu, ses points sont ajoutés au score du joueur
- lorsqu'un ennemi est vraincu, on lance l'animation de destruction
- le nombre de points gagnés apparaît briévement à la place de l'ennemi lorsque celui-ci est vraincu

### Ressources graphiques

- le score courant est affiché quelque part dans le monde? 
- le tableau de fin affiche le score du joueur

### Ressources audio

- Une ambiance sonore est ajoutée au jeu



## Etape 4 

les ennemis spawnent de plusieurs endroits différents sur la plateforme et il peut y avoir plusieurs ennemis en même temps. On doit pouvoir se faire une idée de ce que représente une vague d'ennemis.

### A Implémenter

- ajouter plusieurs spawners tout autour du joueur 
- ajouter une fréquence d'apparition plus ou moins aléatoire (éventuellement qui s'accélère)

### Ressources audios

- son lorsqu'un ennemi est vaincu

## Etape 5

un ennemi volant apparait à distance. Il tire des rayons lasers sur le joueur à une certaine fréquence. Le joueur doit pouvoir se protéger des projectiles.

### A implémenter

- un ennemi volant à distance (immobile) tire des lasers en direction du joueur à une certaine fréquence. 
- le joueur possède un bouclier. Si ce dernier touche un projectile, le projectile est détruit (si c'est un laser, le laser est arrêté)

## Etape #

On multiplie les plateformes. La plateforme centrale possède une zone à protéger entourée par un bouclier. Les plateformes 

