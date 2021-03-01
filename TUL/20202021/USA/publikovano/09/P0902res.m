x=[15,10,10,8,7];
y=[18,14,10,4.5,3.5];


test=0;
for i=1:length(x)
    test=test+(x(i)-y(i))^2/y(i);
end

hranice=chi2inv(0.95,length(x)-1);
if test<hranice
    'H0'
else
    'H1'
end

p_value=1-chi2cdf(test,length(x)-1)