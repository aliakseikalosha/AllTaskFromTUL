clear all
clc

savefile='P0306.mat';
%rovnomìrné rozdìlení, minimum 1, maximum 7, poèet prvkù
x=random('unif',1,7,10000,1);
x=floor(x);
save(savefile,'x');