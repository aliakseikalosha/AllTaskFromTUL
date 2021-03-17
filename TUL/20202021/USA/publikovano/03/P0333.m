clear all
clc

savefile='P0317.mat';
%normální rozděleníů
x=unifrnd(0,10,1000,1);
x=exp(x);
save(savefile,'x');

