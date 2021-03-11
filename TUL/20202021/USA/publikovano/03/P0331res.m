clear all
clc

x=importdata('P0331.mat');
%a) vnitrni hradby
xdolni1=quantile(x,0.25)-1.5*iqr(x)
xhorni1=quantile(x,0.75)+1.5*iqr(x)

%b) z souradnice
xdolni2=mean(x)-3*std(x)
xhorni2=mean(x)+3*std(x)

%c) 0.5 souradnice
xdolni3=median(x)-3*1.483*mad(x)
xhorni3=median(x)+3*1.483*mad(x)

