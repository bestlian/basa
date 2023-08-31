from rest_framework import serializers
from .models import WordType, Word, Definition, Synonym, Antonym, Translation


class WordTypeSerializer(serializers.ModelSerializer):
    class Meta:
        model = WordType
        fields = '__all__'


class WordSerializer(serializers.ModelSerializer):
    class Meta:
        model = Word
        fields = '__all__'


class DefinitionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Definition
        fields = '__all__'


class SynonymSerializer(serializers.ModelSerializer):
    class Meta:
        model = Synonym
        fields = '__all__'


class AntonymSerializer(serializers.ModelSerializer):
    class Meta:
        model = Antonym
        fields = '__all__'


class TranslationSerializer(serializers.ModelSerializer):
    class Meta:
        model = Translation
        fields = '__all__'
