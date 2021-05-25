clear all
clc

x=[-6,-3,-1,0,2,3,5,6,7,8,9,11,12,14,15,18,22,28,32,37,41]; 

%všimněte si, že data jsou nesymetrická
boxplot(x)

%H0 medián je roven 25, H1: medián není roven 25
[p,h]=signtest(x,25,0.05)