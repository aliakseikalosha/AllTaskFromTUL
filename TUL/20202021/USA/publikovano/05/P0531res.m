%P(X>59.6)=0.2119
%pravd�podobnost, �e je men�� ne� 59.6
Pa=1-0.2119;

%pravd�podobnost, �e je men�� ne� 57,2 je 0.7258
Pb=0.7258;

%vypo�tu zskore pro dane pravd�podobnosti a z nich sestav�m rovnice, 
%kter� potom vypo�tu

za=norminv(Pa,0,1);
zb=norminv(Pb,0,1);

%�e��m soustavu rovnic:
%z=(x-my)/sigma pro nezn�m� m� a sigma. 