clear all
clc

vstup=importdata('P0517b.xlsx');
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1

%naèteme 79 vstupních dat
x=vstup.List1(:,1)';

%zkouska ukonèena poruchou, tedy cens=0
%každé dato je jenom jednou
cens(1:79)=0;
freq(1:79)=1;

%pøidání dat s ukonèením zkoušky èasem
x(80)=10000;
cens(80)=1;
freq(80)=21;

%výpoèet
EX=expfit(x,0.05,cens,freq)


