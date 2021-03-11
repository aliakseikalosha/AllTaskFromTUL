clear all
clc

savefile='P0312.mat';
%rovnomìrné rozdìlení, minimum 1, maximum 7, poèet prvkù
x=random('unif',0,1,10000,2);
save(savefile,'x');