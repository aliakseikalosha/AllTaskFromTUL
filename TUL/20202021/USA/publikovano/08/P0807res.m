clear all
clc

x=[35,36,36.3,36.8,37.2,37.6,38.3,39.1,39.3,39.6,39.8;37.2,38.1,38.2,37.9,37.6,38.3,39.2,39.4,39.7,39.9,39.9];
%párový test, zjistíme vliv tepelné úpravy
data=x(2,:)-x(1,:);

%a)H0: mí1=mí2     H1: mí1 se nerovná mí2
[h,p,ci,stat]=ttest(data,0)

%b) H0: mí1>=mí2 neboli mí2-mí1<=0     
%   H1: mí1<mí2 neboli mí2-mí1>0
[h,p,ci,stat]=ttest(data,0,0.05,'right')