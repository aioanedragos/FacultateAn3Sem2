import nltk
nltk.download('vader_lexicon')
from nltk.sentiment.vader import SentimentIntensityAnalyzer
# test_subset=['20170412', 'great', 'bad', 'terrible', 'dog', 'stop', 'good']
def functie(words):
    sid = SentimentIntensityAnalyzer()
    pos_word_list=[]
    neu_word_list=[]
    neg_word_list=[]

    for word in words:
        if (sid.polarity_scores(word)['compound']) >= 0.5:
            pos_word_list.append(word)
        elif (sid.polarity_scores(word)['compound']) <= -0.5:
            neg_word_list.append(word)
        else:
            neu_word_list.append(word)                

    print('Positive :',pos_word_list)        
    # print('Neutral :',neu_word_list)    
    print('Negative :',neg_word_list)

f = open("Text.txt","r",encoding='utf-8')
ceva = f.read()
words = ceva.split(' ')
functie(words)
# print(words)