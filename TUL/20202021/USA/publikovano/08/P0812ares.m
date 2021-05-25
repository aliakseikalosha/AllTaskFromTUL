clear all
clc

%otevøe data uložená v souboru
x=importdata('P0812.mat');
str_hodnota=mean(x)
rozptyl=var(x);

%H0: median = 220
%H1: medián není 220
p_value=2*binocdf(18,length(x),0.5)

if p_value>0.05
    'Hypotézu H0 na hladinì významnosti 5 % nezamítáme'
else
    'Hypotézu H0 na hladinì významnosti 5 % zamítáme'
end