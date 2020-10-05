clear all;clc;
x = [0 1 0 1 0 0 0.5 1 1];
y = [0 0 1 1 0 1 1.5 1 0];
a  = [-2 2 -2 2];
s = [cosd(30),-sind(30);sind(30),cos(30)];
d = [2,0;0,1];
plot(x,y);
hold on
Morphism(s, x,y,a);
Morphism(d, x,y,a);
Morphism([1/2,0;0,1], x,y,a);
Morphism(d*s, x,y,a);
Morphism(s*d, x,y,a);
Morphism(s*s*d*inv(s)^2, x,y,a);
Morphism([0,1;1,0], x,y,a);
Morphism([1,1;1,1], x,y,a);