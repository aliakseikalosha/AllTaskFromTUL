clear all
clc

%otevře data uložená v souboru
x=importdata('P0333.mat');
stredni_hodnota=mean(x)
median=median(x)
hist(x,50)
