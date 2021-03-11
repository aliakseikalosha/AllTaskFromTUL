clear all
clc

savefile='P0305.mat';
%rovnomìrné rozdìlení, minimum 1, maximum 7, poèet prvkù
x=normrnd(10,5,10000,1);
save(savefile,'x');