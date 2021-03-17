%%cv31
clc;clear all
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0331.mat')
fprintf("X0.5%n")
n = length(x);
for i=1:n
    z = abs(x(i) - median(x))/(1.483*mad(x));
    if z > 3
            i
    end
end
%%cv33
clc;clear all;
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0333.mat')
q5 = quantile(x,0.05)
q50 = quantile(x,0.5)
q95 = quantile(x,0.95)
stred = mean(x)
Median = median(x)
cdfplot(x)