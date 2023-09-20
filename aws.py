import boto3

# Profile va Region olish
session = boto3.Session()
profile = session.profile_name
region = session.region_name