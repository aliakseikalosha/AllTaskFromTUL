clear all
clc

%otevøe data uložená v souboru
x=importdata('P0328.mat');
strhod=mean(x)
rozptyl=var(x)
q5procent=quantile(x,0.05)
q50procent=quantile(x,0.50)
q95procent=quantile(x,0.95)
q25procent=quantile(x,0.25)
q75procent=quantile(x,0.75)
q75procent=prctile(x,75)
histogram(x)

cdfplot(x)
