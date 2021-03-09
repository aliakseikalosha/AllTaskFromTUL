str_hod=27400;
sm_odch=5400;
n=50;

test=(str_hod-30000)*sqrt(n)/sm_odch

%porovnání výsledkù
test_dolni=tinv(0.025,n-1);
test_horni=tinv(0.975,n-1);
 
if test<test_horni
    if test>test_dolni
        'H0'
    end
else
    'H1'
end

p_value=tcdf(test,n-1);
if p_value>0.5
    p_value=1-p_value;
end
p_value
test_dolni
test_horni