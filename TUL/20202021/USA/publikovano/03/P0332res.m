clear all
clc

%otevøe data uložená v souboru
x=importdata('P0332.mat');
q5procent=quantile(x,0.05)
q50procent=quantile(x,0.50)
q95procent=quantile(x,0.95)
cdfplot(x)

