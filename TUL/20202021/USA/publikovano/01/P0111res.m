clear all
clc

% jedná se o variace bez opakování, nemùžeme mít shodnou cifru 2x
cisel=10;
cifer=6;

%první pozice je z 10 èísel, druhá z 9 atd. 
vysledek=10*9*8*7*6*5;

%nebo dle vzorce
vysledek=factorial(cisel)/factorial(cisel-cifer)
