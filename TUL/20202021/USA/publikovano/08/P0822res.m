clear all

x=[24,26,27,28,28,28,29,31,32,33];
y=[-21,-5,3,8,14,17,19,21,29,38,46,52,68];

%otestov�n�, zda data jsou z norm�ln�ho rozd�len�
subplot(2,1,1);
normplot(x);
subplot(2,1,2);
normplot(y);

%testov�n�
[h,p,ci,stat]=vartest2(x,y);
if h=='H0'
    'vartype =  equal'
    [h,p,ci,stat]=ttest2(x,y,0.05,'both','equal')
else
    'vartype = unequal'
    [h,p,ci,stat]=ttest2(x,y,0.05,'both','unequal')
end
