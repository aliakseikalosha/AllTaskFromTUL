mu=150;
sd2=16;
n=100;

sd=sqrt(sd2);

mu_min=mu-(sd/sqrt(n))*tinv(0.975,n-1)
mu_max=mu+(sd/sqrt(n))*tinv(0.975,n-1)

sd2_min=((n-1)*sd2)/chi2inv(0.975,n-1)
sd2_max=((n-1)*sd2)/chi2inv(0.025,n-1)