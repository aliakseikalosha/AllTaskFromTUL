% SOUCTCOS.M - ukazka souctu kosinusovek tvoricich jednotkovy impuls
clc;
close all;
clear all;

Fs = 100;
pocet = 1000;



t=-1:1/Fs:1;
s=cos(2*pi*t*0); % nuly
for n = 1:pocet
   s=s+cos(2*pi*t*n);
   plot(t,s);
   title(n);
   pause;
end



