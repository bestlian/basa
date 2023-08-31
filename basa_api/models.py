from django.db import models
import uuid


class WordType(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    name = models.CharField(max_length=50)
    description = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)


class Word(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    word_text = models.CharField(max_length=255)
    word_type = models.ForeignKey(WordType, on_delete=models.CASCADE)
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)


class Definition(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    word = models.ForeignKey(Word, on_delete=models.CASCADE)
    definition_text = models.TextField()
    example = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)


class Synonym(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    word = models.ForeignKey(
        Word, on_delete=models.CASCADE, related_name='synonyms')
    synonym_word = models.ForeignKey(Word, on_delete=models.CASCADE)
    created_at = models.DateTimeField(auto_now_add=True)


class Antonym(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    word = models.ForeignKey(
        Word, on_delete=models.CASCADE, related_name='antonyms')
    antonym_word = models.ForeignKey(Word, on_delete=models.CASCADE)
    created_at = models.DateTimeField(auto_now_add=True)


class Translation(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    source_word = models.ForeignKey(
        Word, on_delete=models.CASCADE, related_name='source_translations')
    target_word = models.ForeignKey(
        Word, on_delete=models.CASCADE, related_name='target_translations')
    target_language = models.CharField(max_length=50)
    translation_text = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
