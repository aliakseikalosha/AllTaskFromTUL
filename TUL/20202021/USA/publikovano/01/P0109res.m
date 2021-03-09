clear all
clc

%záleží na poøadí
%první místo v turnaji lze vybrat z 10 lidí
%druhé místo z 9 lidí atd.

lidi=10;
mist=4;

pocet_zpusobu=factorial(lidi)/factorial(lidi-mist)