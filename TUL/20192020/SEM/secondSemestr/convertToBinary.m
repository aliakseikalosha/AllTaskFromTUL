function [b] = convertToBinary(x, eps)
    maxPow = 0;
    while(x>2^maxPow)
        maxPow=maxPow+1;
    end
    b1 = zeros(1,maxPow);
    for i=maxPow:-1:0
        if(x-2^(i)>0)
            b1(maxPow-i+1) = 1;
            x=x-2^i;
        end
    end
    b2=0;
    i = 1;
    while (2^(-i)>eps && x > 0)
        x=x*2;
        if x>1
            x=x-1;
            b2(i)=1;
        else
            b2(i)=0;
        end
        i=i+1;
    end
    b=[b1,".",b2];
end

