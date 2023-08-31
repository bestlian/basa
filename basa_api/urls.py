from django.urls import path
from rest_framework import permissions
from drf_yasg.views import get_schema_view
from drf_yasg import openapi
# from .views import (
#     DefinitionListCreateView,
#     SynonymListCreateView, AntonymListCreateView, TranslationListCreateView
# )
from .views.wordtype_views import WordTypeListView, WordTypeDetailView
from .views.word_views import WordListView, WordDetailView
from .views.definition_views import DefinitionListView, DefinitionDetailView
from .views.synonym_views import SynonymListView, SynonymDetailView
from .views.antonym_views import AntonymListView, AntonymDetailView
from .views.translation_views import TranslationListView, TranslationDetailView
from rest_framework_simplejwt.views import TokenObtainPairView, TokenRefreshView, TokenVerifyView
from rest_framework_simplejwt.authentication import JWTAuthentication


schema_view = get_schema_view(
    openapi.Info(
        title="BASA API TRANSLATION",
        default_version='v1',
        description="API documentation for BASA",
        # terms_of_service="https://www.yourapp.com/terms/",
        contact=openapi.Contact(email="kanakoding@gmail.com"),
        # license=openapi.License(name="Your License"),
    ),
    public=True,
    permission_classes=(permissions.AllowAny,),
)

urlpatterns = [
    path('words/', WordListView.as_view(), name='word-list'),
    path('words/<uuid:pk>/',
         WordDetailView.as_view(), name='word-detail'),
    path('wordtypes/', WordTypeListView.as_view(), name='wordtype-list'),
    path('wordtypes/<uuid:pk>/',
         WordTypeDetailView.as_view(), name='wordtype-detail'),
    path('definitions/', DefinitionListView.as_view(), name='definition-list'),
    path('definitions/<uuid:pk>/',
         DefinitionDetailView.as_view(), name='definition-detail'),
    path('synonyms/', SynonymListView.as_view(), name='synonym-list'),
    path('synonyms/<uuid:pk>/',
         SynonymDetailView.as_view(), name='synonym-detail'),
    path('antonyms/', AntonymListView.as_view(), name='antonym-list'),
    path('antonyms/<uuid:pk>/',
         AntonymDetailView.as_view(), name='antonym-detail'),
    path('translations/', TranslationListView.as_view(), name='translation-list'),
    path('translations/<uuid:pk>/',
         TranslationDetailView.as_view(), name='translation-detail'),

    # Swagger UI
    path('swagger/', schema_view.with_ui('swagger',
         cache_timeout=0), name='schema-swagger-ui'),
    # ReDoc UI
    path('redoc/', schema_view.with_ui('redoc',
         cache_timeout=0), name='schema-redoc'),

    # ...
    path('token/', TokenObtainPairView.as_view(), name='token_obtain_pair'),
    path('token/refresh/', TokenRefreshView.as_view(), name='token_refresh'),
    path('token/verify/', TokenVerifyView.as_view(), name='token_verify'),
]
