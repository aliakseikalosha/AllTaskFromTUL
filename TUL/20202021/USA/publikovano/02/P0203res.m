clear all
clc

%nutno spoèítat kolik je rohù a stìn ve velké krychli.
krychlicek=5^3;
neobarvena=3^3; 	%vnitøek krychle
jedna=3*3*6; 		%vnitøní krychlièky na každé stìnì * poèet stìn
dva=3*12; 		%poèet krajových*poèet hran krychle
tri=8; 			%poèet rohù

a=neobarvena/krychlicek
b=jedna/krychlicek
c=dva/krychlicek
d=tri/krychlicek


%provedeme pro jistotu kontrolu, že a+b+c+d=1
a+b+c+d
