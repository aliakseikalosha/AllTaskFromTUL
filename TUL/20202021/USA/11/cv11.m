clear all;clc
fprintf("Uloha 816\n")
R=[98.5, 98.6, 98.7, 98.7, 98.7, 98.8, 98.9, 99.2, 99.3, 99.3];
O=[98.1,98.2, 98.3, 98.4, 98.6, 98.7, 98.8, 98.9, 99.0, 99.0];
[h,p,ci,stats] = vartest2(R,O,0.05,'both')
fprintf("Uloha 819\n")
x=[35.0,36.0,36.3,36.8,37.2,37.6,38.3,39.1,39.3,39.6,39.8];
group=[37.2,38.1,38.2,37.9,37.6,38.3,39.2,39.4,39.7,39.9,39.9];
[h,p,ci,stats] = ttest2(x,group,0.05,'both')%,'equal') % 'equal' není potřeba protože je to defaultní hodnota asi
fprintf("Uloha 820\n")
A=[62, 54, 55, 60, 53, 58];
B=[52, 56, 50, 49, 51];
[h,p,ci,stats] = vartest2(A,B,0.05,'both')
[h,p,ci,stats] = ttest2(A,B,0.05,'both')
fprintf("Uloha 823\n")
x=[12,14,16,18,19,19,21,23,25,27,31,35,39,42];
group=[15,18,21,24,27,29,32,35];
[p,h,stats] = ranksum(x,group)
fprintf("Uloha 824\n")

p1 = 325/1240
p2 = 287/741
n1 = 1240
n2 = 741

T=(p1-p2)/sqrt((p1*(1-p1)/n1)+(p2*(1-p2)/n2))
pvalue=2*min(normcdf(T,0,1),1-normcdf(T,0,1))

fprintf("Uloha 825\n")
x1=[18,19,19,19,20,21,21,22,22,23,23,24,24,24,25,25,25,26,26,26,27,28];
x2=[17,18,18,19,19,20,21,21,22,22,22,23,23,23,23,24,24,25,25,26,26,27,28,29];
x3=[16,17,18,18,18,19,20,20,20,20,21,21,21,22,23,23,23,24,25,26,27,27,28,28,29,31];
x4=[14,15,16,16,17,18,19,20,22,22,22,23,24,25,25,27,27,27,28,28,28,31,31,33,34];
x = [x1,x2,x3,x4]';
group = [ones(size(x1)),ones(size(x2))*2,ones(size(x3))*3,ones(size(x4))*4]';
[p,stats] = vartestn(x,group)
[p,tbl,stats] = anova1(x,group)

fprintf("Uloha 826\n")
close all;
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/08/P0826.mat')
group = [ones(1,100),ones(1,100)*2,ones(1,100)*3,ones(1,100)*4,ones(1,100)*4]';
[p,stats] = vartestn(x,group)
[p,tbl,stats] = anova1(x,group)
