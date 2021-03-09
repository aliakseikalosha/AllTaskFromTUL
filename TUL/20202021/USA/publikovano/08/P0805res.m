Spotreba=[8.8, 8.9, 9.0, 8.7, 9.3, 9.0, 8.7, 8.8, 9.4, 8.6, 8.9] 
str_hodnota=mean(Spotreba)
rozptyl=var(Spotreba)

[h,p,ci,stats]=ttest(Spotreba,8.8,0.05,'both')
[h,p,ci,stats]=vartest(Spotreba,0.1,0.05,'both')

