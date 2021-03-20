clear all
clc

%pravdepodobnost hráčů
P1=0;
P2=0;

%budeme uvažovat dvoupokusy
for i=0:1000
    %vyhraje první hráč, celkem lichý počet pokusů
    P1=P1+geopdf(2*i,1/6);
    %vyhraje druhý hráč, celkem sudý počet pokusů
    P2=P2+geopdf(2*i+1,1/6);
end
P1
P2