nDISK=800;
nEMEM=600;

porDISK=24;
porEMEM=14;

pDISK=porDISK/nDISK;
pEMEM=porEMEM/nEMEM;

p=(porDISK+porEMEM)/(nDISK+nEMEM);

roz_min=(pDISK-pEMEM)-sqrt(p*(1-p)*(1/nDISK+1/nEMEM))*norminv(0.975,0,1)
roz_max=(pDISK-pEMEM)+sqrt(p*(1-p)*(1/nDISK+1/nEMEM))*norminv(0.975,0,1)