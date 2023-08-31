from rest_framework import generics
from basa_api.models import Word
from basa_api.serializers import WordSerializer
from drf_yasg.utils import swagger_auto_schema
from drf_yasg import openapi
from rest_framework.decorators import authentication_classes, permission_classes
from rest_framework.permissions import IsAuthenticated
from rest_framework_simplejwt.authentication import JWTAuthentication


@swagger_auto_schema(
    manual_parameters=[
        openapi.Parameter(
            'Authorization', openapi.IN_HEADER, description='Bearer <token>',
            type=openapi.TYPE_STRING
        )
    ]
)
@authentication_classes([JWTAuthentication])
@permission_classes([IsAuthenticated])
class WordListView(generics.ListCreateAPIView):
    queryset = Word.objects.all()
    serializer_class = WordSerializer


@authentication_classes([JWTAuthentication])
@permission_classes([IsAuthenticated])
class WordDetailView(generics.RetrieveUpdateDestroyAPIView):
    queryset = Word.objects.all()
    serializer_class = WordSerializer
