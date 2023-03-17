# basaAPI/serializers
from rest_framework import serializers
from .models import Basa, LANGUAGE_CHOICES, STYLE_CHOICES


class BasaSerializer(serializers.ModelSerializer):
    class Meta:
        model = Basa
        fields = (
            "id",
            "title",
            "code",
            "linenos",
            "language",
            "style",
        )
