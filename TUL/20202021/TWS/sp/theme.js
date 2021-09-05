const THEME_KEY = 'theme';
const THEME_KEY_DARK = 'Dark Mode';
const THEME_KEY_LIGHT = 'Light Mode';
theme = localStorage.getItem(THEME_KEY);
console.log(theme);

themeLable = null;
window.onload = function(){
    console.log("init");
    themeLable = document.getElementById("themeToggle");
    console.log(themeLable);
    if(theme === THEME_KEY_DARK){
        setDarkMode();
    }else{
        setLightMode();
    }
}

function toggleMode(){
    if(theme === THEME_KEY_DARK){
        setLightMode();
    }
    else{
        setDarkMode();
    }
}

function setDarkMode(){
    console.log("setDarkMode");
    /*top*/
	setValue('--top-line','#ff9933');
	setValue('--top-line-text','black');
    /*page*/
	setValue('--text-background','black');
	setValue('--text-color','white');
    /*links*/
	setValue('--link-visited',' #661100');
	setValue('--link-hover',' #ff0033');
	setValue('--link-active',' #33ff33');
    /*table*/
	setValue('--table-accent','#EE812B');
	setValue('--table-white','black');
	setValue('--table-text','#dd1111');
	setValue('--table-under',' white');
    /*article*/
	setValue('--article-strong','purple');
	setValue('--article-abbr','brown');
    /*form*/
	setValue('--form-invalid','red');
	setValue('--form-valid','white');
    setTheme(THEME_KEY_DARK);
}

function setLightMode(){
    console.log("setLightMode");
    /*top*/
	setValue('--top-line','#3399ff');
	setValue('--top-line-text','white');
    /*page*/
	setValue('--text-background','white');
	setValue('--text-color','black');
    /*links*/
	setValue('--link-visited',' #001166');
	setValue('--link-hover',' #3300ff');
	setValue('--link-active',' #33ff33');
    /*table*/
	setValue('--table-accent','#2B81EE');
	setValue('--table-white','white');
	setValue('--table-text','#1111dd');
	setValue('--table-under',' black');
    /*article*/
	setValue('--article-strong','darkred');
	setValue('--article-abbr','darkorange');
    /*form*/
	setValue('--form-invalid','red');
	setValue('--form-valid','black');
    setTheme(THEME_KEY_LIGHT);
}

function setValue(name, value){
    document.documentElement.style.setProperty(name, value);
}

function setTheme(value){

    localStorage.setItem(THEME_KEY,value);
    if(themeLable !== null)
    {
        themeLable.innerHTML = value;
    }
    theme = value;
}