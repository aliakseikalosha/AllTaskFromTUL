clear all
clc

format long
hygepdf(22,17000,10000,30)
binopdf(22,30,10000/17000)

%poznamka, kdybychom měli pouze 1000 bílých a 700 černých
%byly by výsledky u hypergeometrického:
%hygepdf(22,1700,1000,30)=0.040549

%poznamka, kdybychom měli dokonce 100000 bílých a 70000 černých
%byly by výsledky u hypergeometrického:
%hygepdf(22,170000,100000,30)=0.041174

