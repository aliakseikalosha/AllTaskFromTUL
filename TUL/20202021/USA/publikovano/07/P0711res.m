p=59/320;
n=320;

p_min=p-norminv(0.975)*sqrt(p*(1-p)/n)
p_max=p+norminv(0.975)*sqrt(p*(1-p)/n)

n_min=p_min*20000
n_max=p_max*20000

pravd2000=binocdf(2000,20000,p)
pravd2500=binocdf(2500,20000,p)
pravd3000=binocdf(3000,20000,p)
pravd4000=binocdf(4000,20000,p)
pravd4500=binocdf(4500,20000,p)