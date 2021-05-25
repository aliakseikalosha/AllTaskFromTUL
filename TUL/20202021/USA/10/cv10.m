clear all;clc
fprintf("Uloha 801\n")
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/08/P0801.mat')
[h,p,ci,stats] = vartest(x,2.25,0.05,'right')

fprintf("Uloha 802\n")
x=[2.22, 3.54, 2.37, 1.66, 4.74, 4.82, 3.21, 5.44, 3.23, 4.79, 4.85, 4.05, 3.48, 3.89, 4.90, 5.37];
[h,p,ci,stats] = vartest(x,0.6,0.05,'right')

fprintf("Uloha 805\n")
x=[8.8, 8.9, 9.0, 8.7, 9.3, 9.0, 8.7, 8.8, 9.4, 8.6, 8.9];
[h,p,ci,stats] = ttest(x,8.8, 0.05,'both')
[h,p,ci,stats] = vartest(x,0.1, 0.05,'both')

fprintf("Uloha 807\n")
x=[35.0,36.0,36.3,36.8,37.2,37.6,38.3,39.1,39.3,39.6,39.8;37.2,38.1,38.2,37.9,37.6,38.3,39.2,39.4,39.7,39.9,39.9];
rozdil = x(1,:) - x(2,:);
[h,p,ci,stats] = ttest(rozdil,0, 0.05,'both')
[h,p,ci,stats] = ttest(rozdil,0, 0.05,'left')

fprintf("Uloha 808\n")
x=[-6,-3,-1,0,2,3,5,6,7,8,9,11,12,14,15,18,22,28,32,37,41];
[p,h,stats] = signtest(x,25,0.05)

fprintf("Uloha 813\n")
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/08/P0812.mat')
[p,h,stats] = signrank(x,220,0.5,'method', 'exact')

fprintf("Uloha 815\n")
n=1000;
P=82/1000;
p=0.15;
T = (P-p)/sqrt(p*(1-p))*sqrt(1000)
pval= 2*normcdf(T,0.1)
