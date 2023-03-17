# basaAPI/views.py
from rest_framework import generics
from .models import Basa
from .serializers import BasaSerializer


class BasaList(generics.ListCreateAPIView):
    queryset = Basa.objects.all()
    serializer_class = BasaSerializer


class BasaDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = Basa.objects.all()
    serializer_class = BasaSerializer
