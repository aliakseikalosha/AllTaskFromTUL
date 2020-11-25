function y = my_conv(f,g)
    y=zeros(size(f,1)+size(g,1)-1,size(f,2)+size(g,2)-1);
    lg = length(g);
    for i = 1:length(f)
        y(i:i+lg-1) = y(i:i+lg-1)+f(i).*g;
    end
end