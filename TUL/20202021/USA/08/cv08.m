clear all;clc
fprintf("Uloha 617\n")
normcdf(-24.5, 0, 1)
fprintf("Uloha 615\n")
x2015 = [587,124,651,1212,1074,523,273,800,485,961,1683,2411];
x2016 = [121,524,2612,847,1310,1521,951,1000,521,12,190,263,321,587,953];
s15 = mean(x2015);
so15 = var(x2015);
s16 = mean(x2016);
so16 = var(x2016);
normpdf((s15-s16 - 200)/sqrt(so15/12 + so16/15),0,1) % = 0.3779
fprintf("Uloha 622\n")
chi2inv(0.05,10)
chi2inv(0.95,10)
fprintf("Uloha 624\n")
1-chi2cdf(20,12)
fprintf("Uloha 625\n")
1-tcdf(1,[2,4,10,100])
1-normcdf(1,0,1)
fprintf("Uloha 626\n")
tinv(0.05,10)
tinv(0.95,10)
fprintf("Uloha 628\n")
finv(0.05,5,10)
finv(0.95,5,10)

finv(0.05,10,5)
finv(0.95,10,5)

