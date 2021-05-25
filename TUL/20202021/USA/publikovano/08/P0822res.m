clear all
clc

x=[24,26,27,28,28,28,29,31,32,33];
y=[-21,-5,3,8,14,17,19,21,29,38,46,52,68];

%otestování, zda data jsou z normálního rozdìlení
subplot(2,1,1);
normplot(x);
subplot(2,1,2);
normplot(y);

%testování nejdøíve na shodu rozptylù, potom na shodu støedních hodnot
[h,p,ci,stat]=vartest2(x,y)

if h=='H0'
    'vartype =  equal'
    [h,p,ci,stat]=ttest2(x,y,0.05,'both','equal')
else
    'vartype = unequal'
    [h,p,ci,stat]=ttest2(x,y,0.05,'both','unequal')
end
