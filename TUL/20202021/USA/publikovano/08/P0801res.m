prumer=3118;
n=25;
sd=357;
sigma=300;

%výpoèet se provádí ze základního vzorce T=s*s*(n-1)/(sigma*sigma)
test=sd*sd/(sigma*sigma)*(n-1)

%porovnání výsledkù
test_dolni=chi2inv(0.025,n-1);
test_horni=chi2inv(0.975,n-1);
 
if test<test_horni
    if test>test_dolni
        'H0'
    end
else
    'H1'
end

p_value=chi2cdf(test,n-1);
if p_value>0.5
    p_value=1-p_value;
end
p_value
test_dolni
test_horni
