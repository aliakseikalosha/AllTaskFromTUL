clear all
clc

%nutno poèítat podle vzoreèkù, protože nemáme surová data
%H0 sigma=300      HA: sigma se nerovná 300

prumer=3118;
n=25;
sd=357;
sigma=300;

%výpoèet testovací statistiky se provádí ze základního vzorce T=s*s*(n-1)/(sigma*sigma)
test=sd*sd/(sigma*sigma)*(n-1)

%porovnání výsledkù, m
test_dolni=chi2inv(0.025,n-1)
test_horni=chi2inv(0.975,n-1)


%pokud chceme výpis hypotézy 
if (test<test_horni) & (test>test_dolni)
    'Hypotézu H0 na hladinì významnosti 5 % nezamítáme'
else
    'Hypotézu H0 na hladinì významnosti 5 % zamítáme'
end

%pokud chceme výpoèet pvalue
p_value=chi2cdf(test,n-1);
p_value=2*min(p_value,1-p_value)

