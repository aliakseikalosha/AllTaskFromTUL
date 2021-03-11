clear all
clc

%dvakrát nebude eso, jednou bude

%celkem karet   N=32
%vytahuji karet n=3
%poèet es       M=4
%vyberu es      m=1

%první èlen z 28 ne es vybírám dvì karty
%druhý èlen ze 4 es vybírám jedno
%jmenovatel ze 32 karet vybírám 3
P=(nchoosek(28,2)*nchoosek(4,1))/nchoosek(32,3)