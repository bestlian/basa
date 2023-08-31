from rest_framework import generics
from basa_api.models import Translation
from basa_api.serializers import TranslationSerializer
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
class TranslationListView(generics.ListCreateAPIView):
    queryset = Translation.objects.all()
    serializer_class = TranslationSerializer


@authentication_classes([JWTAuthentication])
@permission_classes([IsAuthenticated])
class TranslationDetailView(generics.RetrieveUpdateDestroyAPIView):
    queryset = Translation.objects.all()
    serializer_class = TranslationSerializer
