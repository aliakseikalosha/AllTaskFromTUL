nA=85;
nB=76;

stdA=8;
stdB=6;

varA=stdA*stdA;
varB=stdB*stdB;

%nutno poèítat ze vzorce pro podíl rozptylu. 

podil_var_min=(1/finv(0.975,84,75))*(varA/varB);
podil_var_max=(1/finv(0.025,84,75))*(varA/varB);

podil_std_min=sqrt(podil_var_min)
podil_std_max=sqrt(podil_var_max)