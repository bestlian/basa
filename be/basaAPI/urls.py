# basaAPI/urls.py
from django.urls import path
from rest_framework.urlpatterns import format_suffix_patterns
from basaAPI import views

urlpatterns = [
    path("api/", views.BasaList.as_view()),
    path("api/<int:pk>/", views.BasaDetail.as_view()),
]

urlpatterns = format_suffix_patterns(urlpatterns)
