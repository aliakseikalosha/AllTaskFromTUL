from bs4 import BeautifulSoup
import os
import requests
import re
import json
import time

archive_url = "https://www.idnes.cz/zpravy/archiv/"
path_to_save = "./data/"
index = 357

def get(page_url):
    r = requests.get(page_url)
    if r.ok:
        return r.text
    else:
        time.sleep(5)
        return get(page_url)

def get_soap(page_url):
    return BeautifulSoup(get(page_url), 'html.parser')


def get_list_of_articles(page_index):
    links = list()

    data = get_soap(archive_url + str(page_index)).findAll('a', attrs={'class': 'art-link'})

    for link in data:
        links.append(link['href'])

    return links


def get_title(soap):
    title = None
    try:
        title = soap.find('h1', attrs={'itemprop': 'name headline'})
        if title:
            pass
        else:
            title = soap.find('h1')
    except:
        print("No Title")
    finally:
        return title.text


def get_date(soap):
    date = None
    try:
        d = soap.find('span', attrs={'class': 'time-date'})
        if 'content' in d.attrs:
            date = d.attrs['content']
        else:
            date = d.text
    except:
        print("No Date")
    finally:
        return date


def get_photo_count(soap):
    gallery = soap.find('span', attrs={'class': 'more-gallery'})
    if gallery:
        return gallery.find('b').text
    else:
        return '1'


def get_body_text(soap):
    body_text = None
    try:
        body_text = soap.find('div', attrs={'class': 'opener'}).text
        article_text_part = soap.find('div', attrs={'class': 'bbtext'})
        if article_text_part:
            pass
        else:
            article_text_part = soap
        for p in article_text_part.findAll(['p', 'h3', 'h2']):
            body_text += p.text
    except:
        print("No body text")
    finally:
        return body_text


def get_category(soap):
    category = None
    try:
        category = soap.find('div', attrs={'class': 'portal-g2a'}).find('h3').text
    except:
        print("No Category")
    finally:
        return category


def get_comment_count(soap):
    comments = soap.find('li', attrs={'class': 'community-discusion'})
    if comments:
        return re.sub("[^0-9]", "", comments.find('span').text)
    else:
        return '0'


def download_article(page_url, page_index, article_index):
    print(page_url)
    if page_url.endswith('/foto'):
        page_url = page_url[0:-5]

    soap = get_soap(page_url)
    article = {
        'title': get_title(soap),
        'photo_count': get_photo_count(soap),
        'body_text': get_body_text(soap),
        'date': get_date(soap),
        'category': get_category(soap),
        'comment_count': get_comment_count(soap)
    }

    with open(path_to_save + str(page_index) + '_' + str(article_index) + '.txt', 'w') as outfile:
        json.dump(article, outfile)


def size_of_folder(folder_path):
    # assign size
    size = 0

    # get size
    for path, dirs, files in os.walk(folder_path):
        for f in files:
            fp = os.path.join(path, f)
            size += os.path.getsize(fp)

    return size


while size_of_folder(path_to_save) < 250 * 1024 * 1024:
    i = 0
    for url in get_list_of_articles(index):
        download_article(url, index - 1, i)
        i += 1
    size = size_of_folder(path_to_save)

    print("Downloaded archive page", index, '\nDownloaded', size, '\nThis is ', size * 100 / (250 * 1024 * 1024), '%')
    index += 1
