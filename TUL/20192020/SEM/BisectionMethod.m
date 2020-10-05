function [x, count] = BisectionMethod(a,b,func,eps)
    count = 0;
    if(func(a) == 0)
        x = a;
        return;
    end
    if(func(b) == 0)
        x = b;
        return;
    end
    if(func(a) * func(b) > 0)
        x = NaN;
        return;
    end
    while ((b - a) > eps)
        count = count + 1;
        s =(a+b)/2;
        if func(a) * func(s) < 0
            b = s;
        elseif(func(b)*func(s) <0)
            a = s; 
        else
            x = s;
            return;
        end
    end
    x = (a+b)/2;
end

