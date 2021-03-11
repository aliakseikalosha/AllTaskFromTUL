clear all
clc

savefile='P0309.mat';
%rovnomìrné rozdìlení, minimum 1, maximum 7, poèet prvkù
x=normrnd(10,8,10000,1);
save(savefile,'x');