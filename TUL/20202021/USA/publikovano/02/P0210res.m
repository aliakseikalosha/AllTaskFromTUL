%výpoèteme pravdìpodobnost, že první koule bude èervená, druhá bílá
P2=(4/12)*(3/11);

%pro 3 koule
%první dvì mohou být pouze èervené a èerné ale nesmí být obì èerné
%jevový prostor je èervená èervená; èervená èerná; èernáèervená
%tøetí bílá
P3=(4/12*3/11*3/10)+(4/12*5/11*3/10)+(5/12*4/11*3/10);

%pro 4 koule
%první tøi musí být buï èervené nebo èerné, ale ne všechny èerné
%poslední bílá
P4=(9/12*8/11*7/10-5/12*4/11*3/10)*3/9;

%obdobnì pro další koule
P5=(9/12*8/11*7/10*6/9-5/12*4/11*3/10*2/9)*3/8;
P6=(9/12*8/11*7/10*6/9*5/8-5/12*4/11*3/10*2/9*1/8)*3/7;

P7=(9/12*8/11*7/10*6/9*5/8*4/7)*(3/6+2/5+1/4);
P=P2+P3+P4+P5+P6+P7