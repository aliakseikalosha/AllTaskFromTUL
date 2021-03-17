clear all
clc

%otevøe data uložená v souboru
x=importdata('P0512.mat');
EX=expfit(x)
lambda=1/EX